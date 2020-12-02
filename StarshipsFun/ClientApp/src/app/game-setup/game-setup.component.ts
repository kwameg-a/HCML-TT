import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-game-setup',
  templateUrl: './game-setup.component.html'
})
export class GameSetupComponent implements OnInit {
  choosePlayerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router) { }

  ngOnInit(): void {
    this.choosePlayerForm = this.fb.group({
      player: new FormControl('', Validators.required)
    });
  }

  loadGame() {
    this.router.navigate(['/load-game'], { queryParams: { player: this.choosePlayerForm.controls.player.value } });
  }
}
