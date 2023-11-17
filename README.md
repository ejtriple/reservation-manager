# reservation-manager
GraphQL microservice for Reservation booking between a Provider and a Client.

# Functionality

- Seeds a bit of test data into the "Database" using EF InMemory DB
- Supports GraphQL queries for
    - Providers
    - Appointments
- Supports GraphQL mutations for
    - Creating timeslots for a provider given a date range
    - Reserving an appointment
    - Confirming a reserved appointment
    - Appointment expiration.

# What we're missing
- Unit tests. Ideally would like to interface with the DbContext from the service layer, as this does make mocking a bit more tricky. In the future, I'd break it out.
- Input validation - There is a bit of validation done, but to be robust, would need much more.
- Auth. The API is wide open. Anyone can reserve and create appointments for anyone.
- Flexibility - Appointment slots are always 15 minutes. There is a big lack of filtering / paging that you'd want.
- CI/CD - No docker. No Github actions. No k8s manifest. This is pretty bare bones.

# How to run

```
cd reservation-manager/src/Reservations.API

dotnet restore
dotnet build
dotnet run --project Reservations.API.csproj
```

Open up Banana CakePop (.NET GQL Explorer) at `http://localhost:5001/graphql/`

You can find some pre-set queries in the  docs directory.