import { Component, OnInit, ViewChild, ElementRef, AfterViewChecked } from "@angular/core";
import { ChatMessage } from "../../models/chat-message.model";
import { ChatService } from "../../services/chat.service";
import { interval, Subscription } from "rxjs";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { MessageComponent } from "../message/message.component";
import { MatCardModule } from "@angular/material/card";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatButtonModule } from "@angular/material/button";
import { MatIconModule } from "@angular/material/icon";

@Component({
  selector: 'app-chat-window',
  templateUrl: './chat-window.component.html',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MessageComponent,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule
  ]
})
export class ChatWindowComponent implements OnInit, AfterViewChecked {
  @ViewChild('chatMessages') private chatMessagesContainer!: ElementRef;
  
  messages: ChatMessage[] = [];
  input = '';
  typing = false;
  private typingSub?: Subscription;
  private shouldScroll = false;

  constructor(private chat: ChatService) {}

  ngOnInit() {
    this.chat.getHistory().subscribe(msgs => {
      this.messages = msgs;
      this.shouldScroll = true;
    });
  }

  ngAfterViewChecked() {
    if (this.shouldScroll) {
      this.scrollToBottom();
    }
  }

  private scrollToBottom() {
    try {
      const container = this.chatMessagesContainer.nativeElement;
      container.scrollTop = container.scrollHeight;
      this.shouldScroll = false;
    } catch (err) {}
  }

  send() {
    if (!this.input.trim()) return;
    
    const userMsg = this.input;
    this.input = '';
    this.typing = true;

    // Get bot response
    this.chat.sendMessage(userMsg).subscribe(response => {
      const fullBotResponse = response.botResponse;
      
      const message: ChatMessage = {
        ...response,
        botResponse: ''
      };
      
      this.messages.push(message);
      this.shouldScroll = true;

      let index = 0;
      const chunkSize = 5;
      
      this.typingSub?.unsubscribe();
      this.typingSub = interval(50).subscribe(() => {
        if (index < fullBotResponse.length) {
          message.botResponse = fullBotResponse.substring(0, index + chunkSize);
          index += chunkSize;
          this.shouldScroll = true;
        } else {
          this.typingSub?.unsubscribe();
          this.typing = false;
          this.shouldScroll = true;
        }
      });
    });
  }

  cancelTyping() {
    this.typingSub?.unsubscribe();
    this.typing = false;
  }

  onRate(event: { id: number; rating: boolean | null }) {
    this.chat.rateMessage(event.id, event.rating).subscribe(() => {
      const msg = this.messages.find(m => m.id === event.id);
      if (msg) msg.rating = event.rating;
    });
  }
}
