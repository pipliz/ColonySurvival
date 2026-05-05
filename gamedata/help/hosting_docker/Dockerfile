FROM debian:13-slim

ENV DEBIAN_FRONTEND=noninteractive
ENV HOME=/steamcmd/home

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        bash \
        ca-certificates \
        curl \
        tini \
        libc6-i386 \
    && rm -rf /var/lib/apt/lists/*

RUN useradd --create-home --home-dir /steamcmd/home --shell /bin/bash steam \
    && mkdir -p /steamcmd /server /defaults \
    && chown -R steam:steam /steamcmd /server /defaults

COPY entrypoint.sh /usr/local/bin/entrypoint.sh
COPY server.config.json /defaults/server.config.json

RUN sed -i 's/\r$//' /usr/local/bin/entrypoint.sh \
    && chmod 0755 /usr/local/bin/entrypoint.sh

USER steam
WORKDIR /server

VOLUME ["/steamcmd", "/server"]

EXPOSE 27004/udp 27005/tcp 27005/udp

ENTRYPOINT ["/usr/bin/tini", "--", "/usr/local/bin/entrypoint.sh"]
