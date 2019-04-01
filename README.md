# API_Mashup
To test the API:
* Clone the repository, you can actually clone it directly into Visual studio 2017 from github.
* Open the solution file "APImashup.sln" with visual studio 2017.
* Use IIS Express to run the API. A web-browser will most probably open and
automatically address the URL http://localhost:xxxxx/ (where xxxxx is the port).
* The API can be tested in the browser by specifying resource and passing a mbid:
    * http://localhost:xxxxx/api/artist/{mbid}
    *  for example: http://localhost:55218/api/artist/5b11f4ce-a62d-471e-81fc-a69a8278c7da
* You can inspect the result with the developer tools (f12). 
However, I strongly recommend using Postman for a smoother experience.

Todo:
* Check out compression techniques and different serializers to gain speed.
* Do a more thorough testing to find out were additional error-handling is needed.

Possible issues:
* "Could not find a part of the path '..\bin\roslyn\csc.exe'". Try running this in the package manager: <br />
```Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r```

Pleas let me now if you encounter any issues and I will try to fix them as soon as possible.
