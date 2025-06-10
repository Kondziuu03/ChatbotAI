import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ChatMessage } from '../../models/chat-message.model';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule]
})
export class MessageComponent {
  @Input() message!: ChatMessage;
  @Output() rate = new EventEmitter<{id: number, rating: boolean | null}>();

  rateMessage(rating: boolean) {
    this.rate.emit({
      id: this.message.id,
      rating: this.message.rating === rating ? null : rating
    });
  }
}
