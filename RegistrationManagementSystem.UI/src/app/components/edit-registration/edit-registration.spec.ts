import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditRegistration } from './edit-registration';

describe('EditRegistration', () => {
  let component: EditRegistration;
  let fixture: ComponentFixture<EditRegistration>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditRegistration]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditRegistration);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
