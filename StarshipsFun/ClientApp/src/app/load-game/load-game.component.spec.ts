import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { LoadGameComponent } from './load-game.component';

describe('LoadGameComponent', () => {
  let component: LoadGameComponent;
  let fixture: ComponentFixture<LoadGameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LoadGameComponent],
      imports: [HttpClientTestingModule]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoadGameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
