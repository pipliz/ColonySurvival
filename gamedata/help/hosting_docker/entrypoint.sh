#!/usr/bin/env bash
set -Eeuo pipefail

: "${STEAMCMD_DIR:=/steamcmd}"
: "${STEAMCMD_URL:=https://steamcdn-a.akamaihd.net/client/installer/steamcmd_linux.tar.gz}"
: "${STEAMCMD_LOGIN:=anonymous}"
: "${STEAMCMD_PASSWORD:=}"
: "${STEAMCMD_AUTH_CODE:=}"
: "${SERVER_DIR:=/server}"
: "${SERVER_APP_ID:=748090}"
: "${SERVER_BRANCH:=}"
: "${SERVER_CONFIG_PATH:=/server/server.config.json}"
: "${SERVER_SKIP_UPDATE:=false}"
: "${SERVER_VALIDATE:=false}"
: "${SERVER_EXTRA_ARGS:=}"

log() {
    printf '[entrypoint] %s\n' "$*"
}

log_command() {
    local rendered=""
    local arg

    for arg in "$@"; do
        printf -v rendered '%s%q ' "${rendered}" "${arg}"
    done

    log "Executing: ${rendered% }"
}

is_true() {
    case "${1,,}" in
        1|true|yes|on) return 0 ;;
        *) return 1 ;;
    esac
}

install_steamcmd() {
    if [ -x "${STEAMCMD_DIR}/steamcmd.sh" ]; then
        return
    fi

    log "Bootstrapping SteamCMD into ${STEAMCMD_DIR}"
    mkdir -p "${STEAMCMD_DIR}"
    log "Downloading SteamCMD bootstrap archive from ${STEAMCMD_URL}"
    curl -fsSL "${STEAMCMD_URL}" | tar -xz -C "${STEAMCMD_DIR}"
}

update_server() {
    if is_true "${SERVER_SKIP_UPDATE}"; then
        log "Skipping SteamCMD update because SERVER_SKIP_UPDATE=${SERVER_SKIP_UPDATE}"
        return
    fi

    local -a login_args
    local -a logged_login_args
    login_args=(+login "${STEAMCMD_LOGIN}")
    logged_login_args=(+login "${STEAMCMD_LOGIN}")

    if [ "${STEAMCMD_LOGIN}" != "anonymous" ]; then
        if [ -z "${STEAMCMD_PASSWORD}" ]; then
            log "STEAMCMD_PASSWORD must be set when STEAMCMD_LOGIN is not anonymous"
            exit 1
        fi

        login_args=(+login "${STEAMCMD_LOGIN}" "${STEAMCMD_PASSWORD}")
        logged_login_args=(+login "${STEAMCMD_LOGIN}" "<redacted>")

        if [ -n "${STEAMCMD_AUTH_CODE}" ]; then
            login_args+=("${STEAMCMD_AUTH_CODE}")
            logged_login_args+=("<redacted>")
        fi
    fi

    local -a cmd
    local -a logged_cmd
    cmd=(
        "${STEAMCMD_DIR}/steamcmd.sh"
        +@ShutdownOnFailedCommand 1
        +@NoPromptForPassword 1
        +@sSteamCmdForcePlatformType linux
        +@sSteamCmdForcePlatformBitness 64
        +force_install_dir "${SERVER_DIR}"
        "${login_args[@]}"
        +app_update "${SERVER_APP_ID}"
    )
    logged_cmd=(
        "${STEAMCMD_DIR}/steamcmd.sh"
        +@ShutdownOnFailedCommand 1
        +@NoPromptForPassword 1
        +@sSteamCmdForcePlatformType linux
        +@sSteamCmdForcePlatformBitness 64
        +force_install_dir "${SERVER_DIR}"
        "${logged_login_args[@]}"
        +app_update "${SERVER_APP_ID}"
    )

    if [ -n "${SERVER_BRANCH}" ]; then
        cmd+=(-beta "${SERVER_BRANCH}")
        logged_cmd+=(-beta "${SERVER_BRANCH}")
    fi

    if is_true "${SERVER_VALIDATE}"; then
        cmd+=(validate)
        logged_cmd+=(validate)
    fi

    cmd+=(+quit)
    logged_cmd+=(+quit)

    local attempt
    local max_attempts=2

    for attempt in $(seq 1 "${max_attempts}"); do
        log "Running SteamCMD update for app ${SERVER_APP_ID}${SERVER_BRANCH:+ on branch ${SERVER_BRANCH}} (attempt ${attempt}/${max_attempts})"
        log_command "${logged_cmd[@]}"

        if "${cmd[@]}"; then
            return
        fi

        if [ "${attempt}" -lt "${max_attempts}" ]; then
            log "SteamCMD update attempt ${attempt} failed, retrying once"
        fi
    done

    log "SteamCMD update failed after ${max_attempts} attempts"
    exit 1
}

ensure_install_data_dirs() {
    mkdir -p \
        "${SERVER_DIR}/gamedata/cache" \
        "${SERVER_DIR}/gamedata/logs" \
        "${SERVER_DIR}/gamedata/savegames"
}

ensure_server_config() {
    mkdir -p "$(dirname "${SERVER_CONFIG_PATH}")"

    if [ -f "${SERVER_CONFIG_PATH}" ]; then
        return
    fi

    log "Creating default server config at ${SERVER_CONFIG_PATH}"
    cp /defaults/server.config.json "${SERVER_CONFIG_PATH}"
}

main() {
    install_steamcmd
    mkdir -p "${SERVER_DIR}"
    update_server

    local server_executable="${SERVER_DIR}/colonyserver.x86_64"
    if [ ! -f "${server_executable}" ]; then
        log "Expected server executable not found at ${server_executable}"
        exit 1
    fi

    chmod +x "${server_executable}"
    ensure_install_data_dirs

    ensure_server_config

    local -a extra_args=()
    if [ -n "${SERVER_EXTRA_ARGS}" ]; then
        # shellcheck disable=SC2206
        extra_args=(${SERVER_EXTRA_ARGS})
    fi

    log "Launching Colony Survival dedicated server"
    log_command "${server_executable}" -batchmode -nographics +server.config "${SERVER_CONFIG_PATH}" "${extra_args[@]}" "$@"
    exec "${server_executable}" -batchmode -nographics +server.config "${SERVER_CONFIG_PATH}" "${extra_args[@]}" "$@"
}

main "$@"
