import { Injectable } from "@angular/core";
import { ChatMessage } from "../models/chat-message.model";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({ providedIn: 'root' })
export class ChatService {
  private apiUrl = 'http://localhost:5141/api/chat';

  constructor(private http: HttpClient) {}

  sendMessage(userMessage: string): Observable<ChatMessage> {
    return this.http.post<ChatMessage>(`${this.apiUrl}/send`, { userMessage });
  }

  getHistory(): Observable<ChatMessage[]> {
    return this.http.get<ChatMessage[]>(`${this.apiUrl}/history`);
  }

  rateMessage(id: number, rating: boolean | null): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${id}/rate`, { isPositive: rating });
  }
}