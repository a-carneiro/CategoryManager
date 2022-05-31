# CategoryManager

Service created as tech-Challenge.

I used docker to run Microsoft SQL server, you can use the link below to use the same image that I used.

```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```


To Create de database after initialize the docker image, just run the update-database command on Package Manager Console

Unfortunately I had some personal problems and I didn't finish everything that I want, Like Unit tests for everything and authentication.

The project have 3 endpoint and one controller.

![image](https://user-images.githubusercontent.com/22669298/171101415-00968bf7-fe17-44c0-ab50-ac3f90d0da77.png)

The first is to get the endpoint hierarchy, the second is to add some endpoints.
I followed the provided exemple to insert item. About the depth, on this exemple you can only add one child depth.
for exemplo:

```
{
"Postcards": "5 inch x 6 inch Postcards",
"Postcards": "6 inch x 7 inch Postcards"
}
```
If you want to add a child for "5 inch x 6 inch Postcards" you need to do a new request, like

```
{"5 inch x 6 inch Postcards": "Child"}
```

The following example it is not allowed.

```
{
"Postcards": "5 inch x 6 inch Postcards",
"5 inch x 6 inch Postcards": "6 inch x 7 inch Postcards"
}
```

If i have more time that was a thing that I would like to change to accept a full hierarchy. Maybe using a defined object not dynamic like that.

And the last one is the delete endpoint, you can pass a father name and all children will be deleted.
