import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { PremiumComponent } from './features/premium/premium.component';

@NgModule({
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    AppComponent,
    PremiumComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

