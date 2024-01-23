To reproduce:
```bash
docker compose up -d
dotnet build
dotnet run
psql postgres://test:test@localhost:5433/test -c 'select * from "Test";'
```
