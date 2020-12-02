import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Router } from '@angular/router';

import { AppComponent } from './app.component';
import { GameHomeComponent } from './game-home/game-home.component';
import { GameSetupComponent } from './game-setup/game-setup.component';
import { LoadGameComponent } from './load-game/load-game.component';

import { SharedService } from './shared.service';

const appRoutes = [
  { path: '', component: GameHomeComponent, pathMatch: 'full' },
  { path: 'game-setup', component: GameSetupComponent },
  { path: 'load-game', component: LoadGameComponent },
]

@NgModule({
  declarations: [
    AppComponent,
    GameHomeComponent,
    GameSetupComponent,
    LoadGameComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [SharedService],
  bootstrap: [AppComponent]
})
export class AppModule { }
