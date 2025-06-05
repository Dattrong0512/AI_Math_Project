# AI MATH Project API

## Description

This is a web server project that serves a Web API is built in clean architecture. and other methods to provide for the AI MATH application.

## Database Diagrams

You can view the database diagrams here: [Diagram](https://drive.google.com/file/d/1dCQlsKq4xj6aVVHFX50BjKBlHccZ5VFS/view?usp=sharing).

## Database Design Document(Vietnamese)

You can see the database description documentation here: [Database decription documentation](https://docs.google.com/document/d/1EUYu1MsdauokZdI1mm8KIGOXaniLo3ib/edit)

## Server deployed : [MathAI](https://mathai.id.vn/swagger/index.html)

Instruction for Docker:

#### Build Image:

- _docker build -t \<image-name\> ._

#### Run Container:

- _docker run -d -p 0.0.0.0:8080:8080 \<image-name\>_

# List of completed features

### Login and Register

#### Login

- Login with email and password, including confirmation and password reset functionality.
- Login with Google using OAuth 2.0.

#### Register User

- Register new account user.
- Register new account admin.

### Authentication and authorization with Identity .NET Core

- Perform authentication and authorization with user types such as user and admin based on system login information.

### Chapter Management

#### Chapter Features

- Retrieve all chapters (`GET /api/chapters`).
- View detailed lessons per chapter (`GET /api/chapters/details`).
- Retrieve lessons by grade (`GET /api/chapters/grade/{grade}/details`).

### Enrollment Management

#### Enrollment Features

- Retrieve enrollment info for a user (`GET /api/enrollment/id/{id}`).

### Lesson Management

#### Lesson Features

- Retrieve lessons by grade, chapter, and lesson order (`GET /api/lesson/grade/{grade}/lessonorder/{lessonorder}`).
- Create new lessons (`POST /api/lesson/grade/{grade}/chapter/{chapterorder}`).
- Retrieve lessons by grade and lesson name (`GET /api/lesson/grade/{grade}/lessonname/{lessonname}`).

### Lesson Progress Management

#### Lesson Progress Features

- Retrieve all lesson progress for a user (`GET /id/{id}`).
- Retrieve lesson progress by semester (`GET /id/{id}/semester/{semester}`).
- Update lesson progress (`PATCH /update/lessonprogressID/{idProgress}/learningprogress/{learningProgress}`).

### Exercise Management

#### Exercise Features

- Retrieve all exercise results associated with a specific enrollment ID (`GET /api/exerciseresults/enrollment/id/{id}`).
- Retrieve an exercise detail for a user with a specific enrollment ID (`GET api/exerciseresult/enrollment/id/{id}/exercise/id/{exerciseid}`).

### Question Management

#### Question Features

- Retrieve questions by grade and lesson order (`GET /api/question/grade/{grade}/lessonorder/{lessonorder}`).

### User Management

#### User Features

- Retrieve paginated list of users (Admin only) (`GET /api/user/pageindex/{pageindex}/pagesize/{pagesize}`).
- Retrieve info of the current logged-in user (`GET /api/user/info`).

### Payment Management

#### Payment Features

- Generates a VnPay payment Url for purchasing a study plan (`POST /api/payment/plan/{idPlan}/user/{idUser}`).
- Generates a VnPay payment Url for purchasing token packages (`POST /api/payment/token/{idToken}/user/{idUser}`).
- Retrieve the latest payment information for a specific user (`GET /api/payment/user/{userID}`).
- Retrieve all payment information for a specific user (`GET /api/payment/user/{userID}/all`).

#### Notification Features

- Retrieve all notifications for admin users (`GET /api/notification/all`).
- Retrieve all notifications for the authenticated user (`GET /api/notification/user/all`).
- Retrieve the most recent notification for the authenticated user (`GET /api/notification/user/newest`).
- Send a notification to all users by admin(`POST /api/notification/all`).
- Send a notification to a specific user by their ID (`POST /api/notification/user/{userId}`).
- Update the status of a specific notification from Unread to Read for the authenticated user (`PATCH /api/notification/{notificationId}`).

## Note

This project is in its early stages, and features will be completed soon.
