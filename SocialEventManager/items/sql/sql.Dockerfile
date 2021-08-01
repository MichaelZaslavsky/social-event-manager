FROM mcr.microsoft.com/mssql/server 

ARG PROJECT_DIR=/tmp/devdatabase
RUN mkdir -p $PROJECT_DIR
WORKDIR $PROJECT_DIR
COPY items/sql/InitializeDatabase.sql ./
COPY items/sql/wait-for-it.sh ./
COPY items/sql/entrypoint.sh ./
COPY items/sql/setup.sh ./

USER root
RUN chmod +x ./setup.sh
RUN chmod +x ./wait-for-it.sh

CMD ["/bin/bash", "entrypoint.sh"]