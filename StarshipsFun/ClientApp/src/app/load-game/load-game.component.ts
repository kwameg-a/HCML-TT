import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { IStarship } from '../../interfaces/starship';
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-load-game',
  templateUrl: './load-game.component.html'
})

export class LoadGameComponent implements OnInit {
  chooseAttributeForm: FormGroup;

  starships: IStarship[] = [];
  userStarships: IStarship[] = [];
  currentUserStarship: IStarship = null;
  computerStarships: IStarship[] = [];
  currentComputerStarship: IStarship = null;
  player = '';
  outComeRevealed = false;
  isDrawOutcome = false;
  isGameOver = false;
  handWinnerMessage = '';
  gameWinnerMessage = '';

  constructor(
    private fb: FormBuilder,
    private service: SharedService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.chooseAttributeForm = this.fb.group({
      gameAttribute: new FormControl('', Validators.required)
    });

    this.route.queryParams.subscribe((params: Params) => {
      this.player = params['player'];
    }, err => console.error(err));

    this.getStarships();
  }

  getStarships(): void {
    this.service.getStarships().subscribe(starships => {
    //this.service.getAllStarships().subscribe(starships => {
      this.starships = starships;
      const starshipsAllocation = this.starships.length * 0.5;
      this.userStarships = this.starships.slice(0, starshipsAllocation);
      this.computerStarships = this.starships.slice(starshipsAllocation);
      this.presentFirstPlayerCard();
    }, error => console.error(error));
  }

  presentFirstPlayerCard(): void {
    if (this.player === 'user') {
      this.userPlayHand();
    } else {
      this.computerPlayHand();
    }
  }

  shuffleUserStarshipsPlayHand(): void {
    this.shuffleCards(this.userStarships, 'Human\'s deck shuffled');
  }
  userPlayHand(): void {
    this.currentUserStarship = this.userStarships[this.userStarships.length - 1];
  }

  shuffleComputerStarshipsPlayHand(): void {
    this.shuffleCards(this.computerStarships, 'Computer\'s deck shuffled');
  }
  computerPlayHand(): void {
    this.currentComputerStarship = this.computerStarships[this.computerStarships.length - 1];
    if (this.player === 'computer') {// this ensures the attribute isn't reset when the player query string is 'user'
      const gameAttributes = ['credits', 'speed', 'rating', 'films', 'crew'];
      const attribute = gameAttributes[Math.floor(Math.random() * gameAttributes.length)];
      this.chooseAttributeForm.controls.gameAttribute.setValue(attribute);
    }
  }

  revealOutcome(): void {
    this.isDrawOutcome = false;

    const userCards = this.userStarships;
    const currentUserCard = this.currentUserStarship;

    const computerCards = this.computerStarships;
    const currentComputerCard = this.currentComputerStarship;

    const selectedAttribute = this.chooseAttributeForm.get('gameAttribute').value;
    switch (selectedAttribute) {
      case 'credits':
        if (this.getNumber(currentUserCard.costInCredits) > this.getNumber(currentComputerCard.costInCredits)) {
          this.handWinnerMessage = 'Human user wins hand for a higher Cost in Credits';
          this.popCardFromComputerAndAddToUser(computerCards, userCards, currentComputerCard);
        } else if (this.getNumber(currentUserCard.costInCredits) < this.getNumber(currentComputerCard.costInCredits)) {
          this.handWinnerMessage = 'Computer wins hand for a higher Cost in Credits';
          this.popCardFromUserAndAddToComputer(userCards, computerCards, currentUserCard);
        } else {
          this.isDrawOutcome = true;
        }
        break;
      case 'rating':
        if (this.getNumber(currentUserCard.hyperdriveRating) < this.getNumber(currentComputerCard.hyperdriveRating)) {
          this.handWinnerMessage = 'Human user wins hand for a lower Hyperdrive Rating';
          this.popCardFromComputerAndAddToUser(computerCards, userCards, currentComputerCard);
        } else if (this.getNumber(currentUserCard.hyperdriveRating) > this.getNumber(currentComputerCard.hyperdriveRating)) {
          this.handWinnerMessage = 'Computer wins hand for a lower Hyperdrive Rating';
          this.popCardFromUserAndAddToComputer(userCards, computerCards, currentUserCard);
        } else {
          this.isDrawOutcome = true;
        }
        break;
      case 'speed':
        if (this.getNumber(currentUserCard.topSpeedInMegalights) > this.getNumber(currentComputerCard.topSpeedInMegalights)) {
          this.handWinnerMessage = 'Human user wins hand for a higher Top Speed in Megalights';
          this.popCardFromComputerAndAddToUser(computerCards, userCards, currentComputerCard);
        } else if (this.getNumber(currentUserCard.topSpeedInMegalights) < this.getNumber(currentComputerCard.topSpeedInMegalights)) {
          this.handWinnerMessage = 'Computer wins hand for a higher Top Speed in Megalights';
          this.popCardFromUserAndAddToComputer(userCards, computerCards, currentUserCard);
        } else {
          this.isDrawOutcome = true;
        }
        break;
      case 'films':
        if (currentUserCard.films.length > currentComputerCard.films.length) {
          this.handWinnerMessage = 'Human user wins hand for a higher Number of films';
          this.popCardFromComputerAndAddToUser(computerCards, userCards, currentComputerCard);
        } else if (currentUserCard.films.length < currentComputerCard.films.length) {
          this.handWinnerMessage = 'Computer wins hand for a higher Number of films';
          this.popCardFromUserAndAddToComputer(userCards, computerCards, currentUserCard);
        } else {
          this.isDrawOutcome = true;
        }
        break;
      case 'crew':
        if (this.getNumber(currentUserCard.crewRequired) < this.getNumber(currentComputerCard.crewRequired)) {
          this.handWinnerMessage = 'Human user wins hand for a lower Crew Required';
          this.popCardFromComputerAndAddToUser(computerCards, userCards, currentComputerCard);
        } else if (this.getNumber(currentUserCard.crewRequired) > this.getNumber(currentComputerCard.crewRequired)) {
          this.handWinnerMessage = 'Computer wins hand for a lower Crew Required';
          this.popCardFromUserAndAddToComputer(userCards, computerCards, currentUserCard);
        } else {
          this.isDrawOutcome = true;
        }
        break;
    }

    if (!this.isDrawOutcome) { this.outComeRevealed = true; }
  }

  reShuffleAndPlayHandAgain(): void {
    this.isDrawOutcome = false;
    this.currentComputerStarship = null;
    this.currentUserStarship = null;
    this.shuffleCards(this.userStarships, '');
    this.shuffleCards(this.computerStarships, '');
    this.presentFirstPlayerCard();
  }

  confirmOutCome(): void {
    this.currentComputerStarship = null;
    this.currentUserStarship = null;
    this.chooseAttributeForm.controls.gameAttribute.setValue('');
    this.endGame(this.userStarships, this.computerStarships);
    this.outComeRevealed = false;
  }

  popCardFromUserAndAddToComputer(userCards: IStarship[], computerCards: IStarship[], currentUserCard: IStarship): void {
    userCards.pop();
    computerCards.unshift(currentUserCard);
    computerCards.unshift(computerCards.splice(computerCards.length - 1, 1)[0]);
  }

  popCardFromComputerAndAddToUser(computerCards: IStarship[], userCards: IStarship[], currentComputerCard: IStarship): void {
    computerCards.pop();
    userCards.unshift(currentComputerCard);
    userCards.unshift(userCards.splice(userCards.length - 1, 1)[0]);
  }

  shuffleCards(cards: IStarship[], message: string): void {
    for (let i = cards.length - 1; i > 0; i--) {
      const j = Math.floor(Math.random() * (i + 1));
      const temp = cards[i];
      cards[i] = cards[j];
      cards[j] = temp;
    }
    if (message !== '') { alert(message); }
  }

  endGame(userCards: IStarship[], computerCards: IStarship[]): void {
    if (userCards.length < 1 || computerCards.length < 1) {
      let message = '';
      if (userCards.length > computerCards.length) {
        message = 'Human WINS!!';
      } else {
        message = 'Computer WINS!!';
      }
      this.gameWinnerMessage = `Game Over! ${message}`;
      this.isGameOver = true;
    }
  }

  newGame(): void {
    this.router.navigate(['']);
  }

  getNumber(input: string): number {
    return /[-+]?[0-9]*\.?[0-9]+/.test(input) ? parseFloat(input) : 0;
  }
}

