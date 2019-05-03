import { Component, OnInit } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { AngularFirestore } from "@angular/fire/firestore";
import { defineBase } from "@angular/core/src/render3";
import { Message } from "src/app/models/message";
import { Observable } from "rxjs";
import * as firebase from "firebase";

@Component({
  selector: "app-chat",
  templateUrl: "./chat.component.html",
  styleUrls: ["./chat.component.scss"]
})
export class ChatComponent implements OnInit {
  chatForm = new FormGroup({
    message: new FormControl("", [Validators.required])
  });
  $messages: Observable<Message[]>;

  constructor(private db: AngularFirestore) {}

  ngOnInit() {
    this.$messages = this.db
      .collection<Message>("messages", ref => ref.orderBy("timestamp", "asc"))
      .valueChanges();
  }

  sendMessage() {
    const formValues = this.chatForm.value;
    const m: Message = {
      message: formValues.message,
      timestamp: firebase.firestore.Timestamp.fromDate(new Date())
    };

    const key = this.db.createId();

    this.db
      .collection("messages")
      .doc(key)
      .set(m);
    this.chatForm.reset();
  }
}
