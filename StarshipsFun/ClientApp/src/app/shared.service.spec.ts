import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { SharedService } from './shared.service';
import { of } from 'rxjs';

describe('SharedService', () => {
  let service: SharedService;
  let mockHttp: any;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      // providers: [
      //    { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
      // ]
    });

    mockHttp = jasmine.createSpyObj('mockHttp', ['get']);
    service = new SharedService(mockHttp, 'baseUrl/');
    // service = TestBed.get(SharedService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should have been called with the right URL', () => {
    service.getStarships();
    // mockHttp.get.and.returnValue(of(true));

    expect(mockHttp.get).toHaveBeenCalledWith('baseUrl/starships');
  });
});
