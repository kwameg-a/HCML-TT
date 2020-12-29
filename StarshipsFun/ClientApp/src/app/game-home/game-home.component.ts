import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-game-home',
  templateUrl: './game-home.component.html'
})
export class GameHomeComponent {

  constructor(private router: Router) { }

  goToGameSetup() {
    this.router.navigate(['/game-setup']);
  }
}
