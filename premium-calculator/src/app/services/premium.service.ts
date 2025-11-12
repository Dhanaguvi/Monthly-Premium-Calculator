import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PremiumService {

  private baseUrl = 'https://localhost:7225/api/premium';

  constructor(private http: HttpClient) {}

  getOccupations(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/occupations`);
  }

  calculatePremium(request: any): Observable<any> {
    return this.http.get(`${this.baseUrl}/calculate`, { params: request });
  }
}