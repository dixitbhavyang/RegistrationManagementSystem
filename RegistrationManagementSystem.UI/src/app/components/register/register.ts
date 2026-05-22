import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Auth } from '../../services/auth';
import { State } from '../../services/state';
import { City } from '../../services/city';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  imports: [CommonModule, FormsModule, ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  registerForm: FormGroup;
  states: any[] = [];
  cities: any[] = [];
  selectedFiles: File[] = [];
  selectedHobbies: string[] = [];
  errorMessage: string = '';
  successMessage: string = '';
  isLoading: boolean = false;
  today: string = new Date().toISOString().split('T')[0];

  hobbiesList = ['Reading', 'Coding', 'Travelling', 'Gaming', 'Cooking', 'Sports', 'Music', 'Art'];

  showPassword: boolean = false;
  passwordStrength = {
    hasMinLength: false,
    hasUppercase: false,
    hasLowercase: false,
    hasNumber: false,
    hasSpecial: false,
  };

  constructor(
    private fb: FormBuilder,
    private authService: Auth,
    private stateService: State,
    private cityService: City,
    private router: Router,
  ) {
    this.registerForm = this.fb.group({
      name: ['', Validators.required],
      username: ['', Validators.required],
      password: [
        '',
        [
          Validators.required,
          Validators.pattern(
            '^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$',
          ),
        ],
      ],
      dateOfBirth: ['', Validators.required],
      gender: ['', Validators.required],
      address: ['', Validators.required],
      stateId: ['', Validators.required],
      cityId: ['', Validators.required],
      pincode: ['', [Validators.required, Validators.pattern('^[0-9]{6}$')]],
    });
  }

  ngOnInit(): void {
    this.stateService.getStates().subscribe((states) => (this.states = states));
    this.registerForm.get('password')?.valueChanges.subscribe((value) => {
      const password = value || '';
      this.passwordStrength = {
        hasMinLength: password.length >= 8,
        hasUppercase: /[A-Z]/.test(password),
        hasLowercase: /[a-z]/.test(password),
        hasNumber: /\d/.test(password),
        hasSpecial: /[@$!%*?&]/.test(password),
      };
    });
  }

  isInvalid(field: string): boolean {
    const control = this.registerForm.get(field);
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
    this.registerForm.patchValue({ cityId: '' });
    if (stateId) {
      this.cityService.getCitiesByState(stateId).subscribe((cities) => (this.cities = cities));
    }
  }

  onFileChange(event: any): void {
    const files: FileList = event.target.files;
    this.selectedFiles = Array.from(files);
  }

  onSubmit(): void {
    if (this.registerForm.invalid) return;
    this.isLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

    const formData = new FormData();
    const formValue = this.registerForm.value;

    formData.append('name', formValue.name);
    formData.append('username', formValue.username);
    formData.append('password', formValue.password);
    formData.append('dateOfBirth', formValue.dateOfBirth);
    formData.append('gender', formValue.gender);
    formData.append('address', formValue.address);
    formData.append('stateId', formValue.stateId);
    formData.append('cityId', formValue.cityId);
    formData.append('pincode', formValue.pincode);

    this.selectedHobbies.forEach((hobby) => formData.append('hobbies', hobby));
    this.selectedFiles.forEach((file) => formData.append('files', file));

    this.authService.register(formData).subscribe({
      next: () => {
        this.successMessage = 'Registration successful! Redirecting to login...';
        this.isLoading = false;
        setTimeout(() => this.router.navigate(['/login']), 2000);
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Registration failed. Please try again.';
        this.isLoading = false;
      },
    });
  }

  togglePassword(): void {
    this.showPassword = !this.showPassword;
  }
}
