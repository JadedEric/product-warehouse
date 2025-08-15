# Product Warehouse API

## Setup & Prerequisites
- [x] Install Aspire Workload
- [x] Verify .NET 9 is locally installed
- [x] Verify that Docker Desktop is running locally
- [x] Verify that PostgreSQL is running locally (dev)
- [x] Set up a GitHub repository
- [x] Create a pull_request template for GitHub
- [x] Create a standard pull_request pipeline
- [x] Add this TODO file with a break-down of deliverables
- [x] Check in initial solution structure

## Project Structure
- [x] Set up the base clean architecture
- [x] Add the UI project with Aspire/ Orchestration
- [x] Add the presentation project with FastEndPoints
- [x] Add the application project with services
- [x] Add the infrastructure project with PostgreSQL
- [x] Add the domain project with business logic

## Domain
- [x] Add result pattern
- [x] Add problem details pattern
- [x] Product entity
- [x] Product repository contract
- [x] Problem detail extensions

## Infrastructure
- [x] Add dependency injection file
- [x] Add Entity Framework Core
- [x] Add Dapper
- [x] Add NPGSQL package for dealing with PostgreSQL
- [x] Configure the product repository
- [x] Configure a Dapper database factory implementation
- [x] Set up DBContext
- [x] Expose a static factory builder method in the Product value object
- [x] Build select
- [x] Build insert
- [x] Build select by

## Application
- [x] Add dependency injection file
- [x] Bring in infrastructure dependency
- [x] Add product service contract
- [x] Add data transfer objects
- [x] Add product service
- [x] Fix spelling mistake on IProductRepository
- [x] Update readme.md

## Presentation
- [ ] Add dependency injection file
- [ ] Add FastApiEndpoints
- [ ] Add GET /products
- [ ] Add GET /products/{id}
- [ ] Add POST /products
- [ ] Add PUT /products/{id}
- [ ] Add DELETE /products/{id}

## WebAPI
- [ ] Add a web api project
- [ ] Expose all end-points from the presentation layer
- [ ] Swagger-type documentation

## UI
- [ ]

## Integration Tests
- [x] Develop a set of integration tests for the infrastructure layer

## Aspire Configuration
- [ ]

## Documentation
- [x] Update README.md

## Issues and Blockers
- [ ] Not going to get to the UI layer
- [ ] Added a WebAPI service for now
- [ ] No unit tests for any of the other layers