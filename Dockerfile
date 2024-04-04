FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /ExpenseTracking

# Copy everything
COPY . ./

#Restore as distinct layers
RUN dotnet restore 

# Build and publish a release
RUN dotnet publish -c Release -o out

RUN ls /ExpenseTracking

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /ExpenseTracking
COPY --from=build /ExpenseTracking/out/. .
ENTRYPOINT ["dotnet", "./ExpenseTracking/out/ExpenseTracking.Api.dll"]