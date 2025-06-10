import 'zone.js';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic'; 
import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideHttpClient } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { importProvidersFrom } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';

bootstrapApplication(AppComponent, {
  providers: [
    importProvidersFrom(
      BrowserModule,
      FormsModule,
      MatCardModule,
      MatButtonModule,
      MatInputModule,
      MatFormFieldModule,
      MatIconModule
    ),
    provideHttpClient(),
    provideAnimations()
  ]
}).catch(err => console.error('Error bootstrapping app:', err));
