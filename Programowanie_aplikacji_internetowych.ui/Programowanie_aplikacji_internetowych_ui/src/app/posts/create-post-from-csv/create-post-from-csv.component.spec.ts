import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatePostFromCsvComponent } from './create-post-from-csv.component';

describe('CreatePostFromCsvComponent', () => {
  let component: CreatePostFromCsvComponent;
  let fixture: ComponentFixture<CreatePostFromCsvComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreatePostFromCsvComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreatePostFromCsvComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
