#!/bin/sh
# Wait for SQL Server to be started and then run the sql script
./wait-for-it.sh sql-server-database:1433 --timeout=0 --strict -- sleep 5s && \
/opt/mssql-tools/bin/sqlcmd -S localhost -i InitializeDatabase.sql -U sa -P "$SA_PASSWORD"