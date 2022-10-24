.PHONY: infra
infra:
	docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=YourSTRONG!Passw0rd' -p 1401:1433 --name sql1 -d mcr.microsoft.com/mssql/server:2022-latest