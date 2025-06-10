import { Component, OnInit } from "@angular/core";
import { ChatWindowComponent } from "./components/chat-window/chat-window.component";
import { CommonModule } from "@angular/common";

@Component({
  selector: 'app-root',
  template: `
    <div class="app-container">
      <app-chat-window></app-chat-window>
    </div>
  `,
  styles: [`
    .app-container {
      min-height: 100vh;
      display: flex;
      justify-content: center;
      align-items: center;
      padding: 20px;
    }
  `],
  standalone: true,
  imports: [CommonModule, ChatWindowComponent]
})
export class AppComponent implements OnInit {
  ngOnInit() {
    console.log('AppComponent initialized');
  }
}