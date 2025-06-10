export interface ChatMessage {
    id: number;
    userMessage: string;
    botResponse: string;
    rating: boolean | null;
    createdAt: string;
  }