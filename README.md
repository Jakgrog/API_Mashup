# API_Mashup
To test the API:
* Open the solution file "APImashup.sln" with visual studio 2017.
* Use IIS Express to run the website. A web-browser will most probably open and
automatically address the URL http://localhost:xxxxx/ (where xxxxx is the port).
* The API can be tested in the browser by specifying resource and passing a mbid:
    * http://localhost:xxxxx/api/artist/{mbid}
    *  for example: http://localhost:55218/api/artist/5b11f4ce-a62d-471e-81fc-a69a8278c7da
* You can inspect the result with the developer tools (f12). 
However, I strongly recommend using Postman for a smoother experience.
