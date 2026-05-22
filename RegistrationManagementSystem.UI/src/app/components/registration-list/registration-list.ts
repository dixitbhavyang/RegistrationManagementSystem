import { Component } from '@angular/core';
import { Registration } from '../../services/registration';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Auth } from '../../services/auth';

@Component({
  selector: 'app-registration-list',
  imports: [CommonModule, FormsModule],
  templateUrl: './registration-list.html',
  styleUrl: './registration-list.css',
})
export class RegistrationList {
  registrations: any[] = [];
  totalCount: number = 0;
  page: number = 1;
  pageSize: number = 10;
  sortBy: string = '';
  filterByName: string = '';
  totalPages: number = 1;
  isAdmin: boolean = false;

  constructor(
    private registrationService: Registration,
    private authService: Auth,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.isAdmin = this.authService.isAdmin();
    this.loadRegistrations();
  }

  loadRegistrations(): void {
    this.registrationService
      .getAll(this.page, this.pageSize, this.sortBy, this.filterByName)
      .subscribe({
        next: (response) => {
          this.registrations = response.items;
          this.totalCount = response.totalCount;
          this.totalPages = Math.ceil(this.totalCount / this.pageSize);
        },
        error: (err) => console.error(err),
      });
  }

  onFilterChange(): void {
    this.page = 1;
    this.loadRegistrations();
  }

  onPageSizeChange(): void {
    this.page = 1;
    this.loadRegistrations();
  }

  changePage(newPage: number): void {
    if (newPage < 1 || newPage > this.totalPages) return;
    this.page = newPage;
    this.loadRegistrations();
  }

  getPages(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  deleteRegistration(id: number): void {
    if (confirm('Are you sure you want to delete this registration?')) {
      this.registrationService.delete(id).subscribe({
        next: () => this.loadRegistrations(),
        error: (err) => console.error(err),
      });
    }
  }

  downloadDocument(documentId: number, fileName: string): void {
    this.registrationService.downloadDocument(documentId).subscribe({
      next: (blob) => {
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = fileName;
        a.click();
        window.URL.revokeObjectURL(url);
      },
      error: (err) => console.error(err),
    });
  }

  editRegistration(id: number): void {
    this.router.navigate(['/edit', id]);
  }

  viewDocument(documentId: number, fileName: string, contentType: string): void {
    this.registrationService.downloadDocument(documentId).subscribe({
      next: (blob) => {
        const url = window.URL.createObjectURL(blob);
        window.open(url, '_blank');
      },
      error: (err) => console.error(err),
    });
  }
}
