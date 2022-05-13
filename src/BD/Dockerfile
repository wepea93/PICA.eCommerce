# docker build -t db_products_mssql -f Dockerfile.Linux .
FROM mcr.microsoft.com/mssql/server:2019-latest

USER root

COPY . .

ARG sa_password=CorrectHorseBatteryStapleFor$
ARG accept_uela=Y
ARG mssql_pid=Express

ENV MSSQL_SA_PASSWORD $sa_password
ENV ACCEPT_EULA=$accept_uela
ENV MSSQL_PID=$mssql_pid
ENV MSSQL_PORT=1433

EXPOSE $MSSQL_PORT

RUN chmod +x entrypoint.sh
ENTRYPOINT [ "/bin/bash", "entrypoint.sh" ]
CMD [ "/opt/mssql/bin/sqlservr" ]