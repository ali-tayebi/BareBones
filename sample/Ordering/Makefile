#!/bin/sh -ex

mg-clean:
ifdef name
	rm -rf Ordering.Infrastructure/Migrations/*.*
	docker stop $(name)
	docker rm $(name)
else
	echo "Please pass the sql-server docker name e.g. name=sql1"
endif

mg-add:
	dotnet ef migrations add $(name) \
		--startup-project Ordering.API  \
		--context Ordering.Persistence.OrderingDbContext \
		--project Ordering.API \
		--output-dir Infrastructure/DB/Migrations


db-update:
	dotnet ef database update  \
		--startup-project Ordering.API  \
		--context Ordering.Persistence.OrderingDbContext \
		--project Ordering.API

db-server:
	docker run \
		-e "ACCEPT_EULA=Y" \
		-e "SA_PASSWORD=Pass@word" \
		-p 1433:1433 --name $(name) \
		-d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
