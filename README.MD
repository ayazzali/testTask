## At this repository sitting' test task completed)
Well it works but it can be a lot better if i asked some 'business' questions. And it still has a lot of todos.

### Some principles
i use mongo in memory because instead of redis cause redis cannot filter by different fileds 

### How to start
```shell
docker run --rm -p 27017:27017 -d mongodb/mongodb-enterprise-server:4.4.20-ubi8 --storageEngine=inMemory
dotnet run --project FlightsApi
```
#### TODOS or questions:
- add oidc?
- how would u like to use cache afterwards
- TimeLimit is a Date with time in UTC or timespan. I would prefer first one cause it gets more obvious implementstion
- separate business with Providers to different libs  
- add some swagger description
- add new mock server that will get u test data
- add unit tests
- add integration' test
- add compose file
- add background task that deletes expired routes from cache
- add logs with scope values