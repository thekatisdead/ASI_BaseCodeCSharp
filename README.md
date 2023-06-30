
# HR Tracking Application (v0.0.1)

Heya! I am here to guide you on your path to understanding our application so that you (yes you) can easily look for potential applicants that not only has the quality you'll need but also efficently handle out the hiring process for your applicants!

In this document, we will be tackling on the functionalities for the following stakeholders
- General
- Applicant
- HR

In our curent implementation of our application, the following guide flowchart will best describe our project flow.

![Flowchart of the HR Tracking Application](/.ignore/img/flowchart.png)

Please take note that some functionalities are present but not fully defined. As such, they do not have a corresponding anchor for the meanwhile. These will be worked on as we progress through the project.

## General Side
- `Home / Landing Page`
    
    This is the first webpage that loads up when you open up the application. This introduces you to what the application can do as well doubling as a way to get your application status.
	
- `Login`

    This is a site that would take the username and password and redirects to the corresponding dashboard; if the user is an Applicant, he will be redirected to the Applicant Dashboard; if the user is part of the HR, he will be redirected to the HR Dashboard.
	
	For this version, it will go directly to the `HR Dashboard` once we click the login button. It will be using the `Username` input field for our `HR Dashboard`
- `Sign-up`

    This is where a new user will be applying to the site. This will allow the Applicant to quickly see their statuses and the HR to facilitate the hiring process.
	
	It is important to take note that most of the input fields found in the Sign-Up pages do not mean anything because it is not connected to a Model. For this version, we will only be using the `Username` input field as this will be used in the `HR Dashboard`

## Applicant Side
- `Applicant Tracking`

    What if you just want to check your status in a job offer but don't want to log in? The Applicant Tracking Modal will help you see the status of your application on the home page without having to log in.
	
	This takes in the `ID` that comes from the `Landing Page` and uses that to return the status of the `ID`. Since this page is not connected to a Model, only the `ID` text box changes value.

## HR Side
- `HR Dashboard`

    The HR Dashboard is where the HR can see all his job postings and  the applicant lists associated to them, as well as handling part the hiring process.

	Note that `Edit` and `Delete` buttons do not work right now, as well as the addition of new Job Postings as we do not have a model connected to it. It is also to take note that `Sessions` component is not present in our project and thus the username does not retain throughout multiple instances of the webpage.
	
- `Add Job Posting`

    This page adds a job post associated to the HR team.
	
	For this version, clicking the `Add Job` button redirects you to the `HR Dashboard` without bringing any of the input fields with it. These will be fully defined once we have Models in our project.
	
- `Applicant List`

    The Applicant List shows all the applicants that have given their application to the job listing, as well as their corresponding status.
	
	Take note that `View Profile` and `Update Status` are features that are present but not yet fully defined.

