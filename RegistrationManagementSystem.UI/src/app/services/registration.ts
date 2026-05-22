import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Auth } from './auth';

@Injectable({
  providedIn: 'root',
})
export class Registration {
  private apiUrl = `${environment.apiUrl}/Registration`;

  constructor(
    private http: HttpClient,
    private authService: Auth,
  ) {}

  private getHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    return new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
  }

  getAll(page: number, pageSize: number, sortBy?: string, filterByName?: string): Observable<any> {
    let params = new HttpParams().set('page', page.toString()).set('pageSize', pageSize.toString());

    if (sortBy) params = params.set('sortBy', sortBy);
    if (filterByName) params = params.set('filterByName', filterByName);

    return this.http.get(this.apiUrl, {
      headers: this.getHeaders(),
      params,
    });
  }

  getById(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`, {
      headers: this.getHeaders(),
    });
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`, {
      headers: this.getHeaders(),
    });
  }

  update(id: number, formData: FormData): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, formData, {
      headers: this.getHeaders(),
    });
  }

  downloadDocument(documentId: number): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/document/${documentId}`, {
      headers: this.getHeaders(),
      responseType: 'blob',
    });
  }
}
