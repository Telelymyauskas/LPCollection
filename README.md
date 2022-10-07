# LP Collection Service 
This repository describes LP Collection API

## User Stories
- As a User I want to create an empty colletion of LP records in stock 
- As a User I want to add/remove records to/from my collection
- As a User I want to create a wishlist of records 
- As a User I want to add/remove records from Wishlist to In stock and vica versa
- As a User I want to be sure that one record connot be in both lists at the same time 
- As a User I want to be sure that one record cannot be added to one list twice

## Terminology
| Term | Definition |
| ------ | ------ |
| LPC | Long play collection (vinyl records) |
| Wishlist | the list of the records that User wants to buy in future|
| LP Collection| Records that User already has |

## HTTP API
- GET /web/homepage -> Returns 201 
- GET /web/collection={collectiontype} -> Returns 201 
- PUT /web/add-to-wishlist/{recordid} -> Returns 201 / 400 Bad request if chosen record has alredy been added to one of lists
- PUT /web/add-to-library/{recordid} -> Returns 201 / 400 Bad request if chosen record has alredy been added to one of lists
- DELETE /api/present/{presentId} -> Returns 204 No content
- DELETE 

## MODELS
Record 
- id
- artist
- album
- releaseDate
- img URL

Library
-id
-recordOwned

Wishlist
-id
-recordWished