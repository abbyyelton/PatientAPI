# Patient Backend API

This project contains a login controller and a patient controller. Bearer token authenitcation is used. There are two valid emails that can be use for log in:
`test@email.com` and `test1@email.com`. Once a user logs in, a token is stored in the 'database' mapped to their email. This token must match any further requests 
from the user. With the token, they can access the /patient and /patient/{patientId} endpoints. When the patients are returned to the user, a safe guid is
returned instead of the true guid. This mapping is also stored in the 'database.' 

## What should be added for a "real" project

 * Unit tests
 * Real authentication/authorization
 * Automapper or similar for mapping guids/models if there are to be more
 * Real database/data storage

## Requirements

API:
1. Create a new .NET project that will host three endpoints:
   1. GET /patients
   2. GET /patients/{patientId}
   3. POST /login (This should take in an email and password and return the auth information else 401 if the creds are invalid. You may stub/hardcode this.)
2. Endpoints should be protected by authentication. You may stub/hardcode this but callers must provide an auth header of the form: "Bearer {authToken}"
3. The patient endpoints should internally call into a patient-service API that we have provided for you at the following url: https://ti-patient-service.azurewebsites.net
   1. Fetch the list of patients by sending a GET request to /patients
   2. Fetch a specific patient by sending a GET request to /patient/{patientId}
   3. There is no authentication for these endpoints
4. Your API project should not expose the patient IDs from the patient-service API. Provide an alternative method of addressing specific patients that your front end will use when calling your API.
5. Make use of dependency injection and class interfaces. You can use whatever DI framework you wish.
6. Make use of a configuration file to store the URL for the patient-service API (and any other values you wish). This should be the only place in your code where the URL for the patient-service is stored.
7. Separate the code for calling the patient-service API into its own class with an interface. Register this class to be used with your dependency injection framework and use that injection in your code where appropriate.
8. Your application can be structured in anyway that you choose, other than these requirements.  


Front End:
1. Using Create React App, generate a new project with at least two views
   1. Login screen that takes in email and password (this should call your /login endpoint)
   2. Patient view (you can redirect here on successful login)
2. Display a list of patients returned
3. When a patient is clicked, show a view of that specific patient (you can model this in whatever way you choose and think makes sense in your design)
4. Use Formik for form state management
5. Use MUI components when building your views
