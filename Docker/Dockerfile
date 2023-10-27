FROM mcr.microsoft.com/mssql/server:2022-latest

# Create a config directory
RUN mkdir -p /usr/config
WORKDIR /usr/config

ENV SA_PASSWORD !@234lKj
ENV ACCEPT_EULA Y
ENV MSSQL_PID Express

# Bundle config source
COPY --chown=mssql . /usr/config
COPY --chmod=755 entrypoint* /usr/config
COPY --chmod=755 configure-db* /usr/config

ENTRYPOINT ["bash","./entrypoint.sh"]