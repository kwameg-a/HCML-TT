import { ComponentFixture, fakeAsync, TestBed } from '@angular/core/testing';
import { Location } from "@angular/common";
import { RouterTestingModule } from "@angular/router/testing";
import { Router } from '@angular/router';

import { GameSetupComponent } from './game-setup.component';

describe('GameSetupComponent', () => {
  let location: Location;
  let router: Router;
  let component: GameSetupComponent;
  let fixture: ComponentFixture<GameSetupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GameSetupComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    router = TestBed.get(Router);
    location = TestBed.get(Location);
    fixture = TestBed.createComponent(GameSetupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('loadGame() when called should navigate to LoadGameComponent', fakeAsync(() => {
    component.loadGame();
    expect(router.navigate).toHaveBeenCalledWith(['/load-game']);
    expect(location.path()).toBe("/load-game");
  }));
});
