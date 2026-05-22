import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Registration } from '../../services/registration';
import { State } from '../../services/state';
import { City } from '../../services/city';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit-registration',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './edit-registration.html',
  styleUrl: './edit-registration.css',
})
export class EditRegistration {
  editForm: FormGroup;
  states: any[] = [];
  cities: any[] = [];
  selectedHobbies: string[] = [];
  errorMessage: string = '';
  successMessage: string = '';
  isLoading: boolean = false;
  today: string = new Date().toISOString().split('T')[0];
  registrationId: number = 0;

  hobbiesList = ['Reading', 'Coding', 'Travelling', 'Gaming', 'Cooking', 'Sports', 'Music', 'Art'];

  constructor(
    private fb: FormBuilder,
    private registrationService: Registration,
    private stateService: State,
    private cityService: City,
    private router: Router,
    private route: ActivatedRoute,
  ) {
    this.editForm = this.fb.group({
      name: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      gender: ['', Validators.required],
      address: ['', Validators.required],
      stateId: ['', Validators.required],
      cityId: ['', Validators.required],
      pincode: ['', [Validators.required, Validators.pattern('^[0-9]{6}$')]],
    });
  }

  ngOnInit(): void {
    this.registrationId = Number(this.route.snapshot.paramMap.get('id'));
    this.stateService.getStates().subscribe((states) => (this.states = states));
    this.loadRegistration();
  }

  loadRegistration(): void {
    this.registrationService.getById(this.registrationId).subscribe({
      next: (reg) => {
        this.selectedHobbies = reg.hobbies ? reg.hobbies.split(',') : [];
        this.cityService
          .getCitiesByState(reg.stateId)
          .subscribe((cities) => (this.cities = cities));
        this.editForm.patchValue({
          name: reg.name,
          dateOfBirth: new Date(reg.dateOfBirth).toISOString().split('T')[0],
          gender: reg.gender,
          address: reg.address,
          stateId: reg.stateId,
          cityId: reg.cityId,
          pincode: reg.pincode,
        });
      },
      error: (err) => console.error(err),
    });
  }

  isInvalid(field: string): boolean {
    const control = this.editForm.get(field);
    return !!(control?.invalid && control?.touched);
  }

  isHobbySelected(hobby: string): boolean {
    return this.selectedHobbies.includes(hobby);
  }

  onHobbyChange(hobby: string, event: any): void {
    if (event.target.checked) {
      this.selectedHobbies.push(hobby);
    } else {
      this.selectedHobbies = this.selectedHobbies.filter((h) => h !== hobby);
    }
  }

  onStateChange(event: any): void {
    const stateId = event.target.value;
    this.cities = [];
    this.editForm.patchValue({ cityId: '' });
    if (stateId) {
      this.cityService.getCitiesByState(stateId).subscribe((cities) => (this.cities = cities));
    }
  }

  onSubmit(): void {
    if (this.editForm.invalid) return;
    this.isLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

    const formData = new FormData();
    const formValue = this.editForm.value;

    formData.append('name', formValue.name);
    formData.append('dateOfBirth', formValue.dateOfBirth);
    formData.append('gender', formValue.gender);
    formData.append('address', formValue.address);
    formData.append('stateId', formValue.stateId);
    formData.append('cityId', formValue.cityId);
    formData.append('pincode', formValue.pincode);
    this.selectedHobbies.forEach((hobby) => formData.append('hobbies', hobby));

    this.registrationService.update(this.registrationId, formData).subscribe({
      next: () => {
        this.successMessage = 'Registration updated successfully!';
        this.isLoading = false;
        setTimeout(() => this.router.navigate(['/registrations']), 2000);
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Update failed. Please try again.';
        this.isLoading = false;
      },
    });
  }

  goBack(): void {
    this.router.navigate(['/registrations']);
  }
}
