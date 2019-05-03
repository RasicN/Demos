# Step 2
## Create Chat Component
1) Create a 'components' folder at `src/app/`

2) Create a ChatComponent `ng g c Chat` at `src/app/components`

3) Add chat component to `app.component.html`

```html
<app-chat></app-chat>
```
4) Create Message model `src/app/models/message.ts`
```typescript
import { Timestamp } from '@firebase/firestore-types';

export class Message {
  public message: string;
  public timestamp: Timestamp;
}
```

5) Add code for chat component

`src/app/app.module.ts`
```typescript
import { ReactiveFormsModule } from '@angular/forms';
```
```typescript
imports: [
  ...,
  ReactiveFormsModule
],
  ```

`src/app/components/chat/chat.component.html`
```html
<div style="min-height: 300px; border: 1px solid black;S">
  <div *ngFor="let message of $messages | async">

  </div>
</div>

<form formGroup="chatForm" (submit)="sendMessage()">
  <input type="text" placeholder="Type your message" formControlName="message">
  <button type="submit">Send</button>
</form>

```

`src/app/components/chat/chat.component.ts`
```typescript
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AngularFirestore } from '@angular/fire/firestore';
import { defineBase } from '@angular/core/src/render3';
import { Message } from 'src/app/models/message';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {

  chatForm = new FormGroup({
    message: new FormControl('', [Validators.required])
  });
  $messages: Observable<Message[]>;

  constructor(private db: AngularFirestore) { }

  ngOnInit() {

  }

  sendMessage() {

  }
}
```

6) Implement send message method
`src/app/components/chat/chat.component.ts`
```typescript
  sendMessage() {
    const formValues = this.chatForm.value;
    const m: Message = {
      message: formValues.message,
      timestamp: firebase.firestore.Timestamp.fromDate(new Date())
    };

    const key = this.db.createId();

    this.db.collection('messages').doc(key).set(m);
    this.chatForm.reset();
  }
```
7) Implement read messages
`src/app/components/chat/chat.component.ts`
```typescript
  ngOnInit() {
    this.$messages = this.db
      .collection<Message>("messages", ref => ref.orderBy("timestamp", "asc"))
      .valueChanges();
  }
```
