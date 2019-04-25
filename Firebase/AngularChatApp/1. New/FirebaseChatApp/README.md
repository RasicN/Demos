# FirebaseChatApp

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 7.1.4.

## Setup Firebase
1) Run in terminal `firebase login` and complete login process
2) Go to https://console.firebase.google.com and create a new firebase project called 'chat-app'
3) Run in terminal `firebase init`
    - Select Firestore, Functions, Hosting, Storage
4) If you have created your 'chat-app' project it should show up in the list of projects for this directory.  Select it.
5) Continue through the rest of the prompts (I left them at defaults and chose Typescript)
6) Install Dependencies
7) Update `firebase.json`

```
"hosting": {
    "public": "public",
    "ignore": [
      "firebase.json",
      "**/.*",
      "**/node_modules/**"
    ],
    "rewrites": [
      {
        "source": "**",
        "destination": "/index.html"
      }
    ]
  },
```
To
```
"hosting": {
    "predeploy":"npm run build",
    "public": "dist/FirebaseChatApp",
    "ignore": [
      "firebase.json",
      "**/.*",
      "**/node_modules/**"
    ],
    "rewrites": [
      {
        "source": "**",
        "destination": "/index.html"
      }
    ]
  },
```
Things to note here are the "predeploy" and "public" sections.  We added a predeploy command that makes the project build before we deploy. Then made sure to point our "public" directory to the output location of the build.

## Setup Angular App
1) Install [AngularFire2](https://github.com/angular/angularfire2)

`npm install firebase @angular/fire --save`

2) Setup AngularFire2 by following steps 3-6 [here](https://github.com/angular/angularfire2/blob/master/docs/install-and-setup.md)

At this point we are setup and ready to start using firebase.  Up next we will be adding in basic UI elements and functionality for our chat app.