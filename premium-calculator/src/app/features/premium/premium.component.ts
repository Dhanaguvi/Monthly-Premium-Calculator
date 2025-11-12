import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PremiumService } from '../../services/premium.service';

@Component({
  selector: 'app-premium',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './premium.component.html',
  styleUrls: ['./premium.component.css']
})
export class PremiumComponent implements OnInit {

  name: string = '';
  ageNextBirthday: number = 0;
  dateOfBirth: string = '';
  occupation: string = '';
  deathSumInsured: number = 0;
  occupations: any[] = [];
  premiumResult: any;

  constructor(private premiumService: PremiumService) {}

  ngOnInit(): void {
    this.loadOccupations();
  }

  loadOccupations(): void {
    this.premiumService.getOccupations().subscribe({
      next: (data: any[]) => this.occupations = data,
      error: (err: any) => console.error('Error loading occupations:', err)
    });
  }

  calculatePremium(): void {
    if (!this.name || !this.occupation || this.ageNextBirthday <= 0 || this.deathSumInsured <= 0) {
      alert('Please fill all required fields.');
      return;
    }

    const params = {
      Name: this.name,
      AgeNextBirthDay: this.ageNextBirthday,
      Occupation: this.occupation,
      DeatchSumInsured: this.deathSumInsured
    };

    this.premiumService.calculatePremium(params).subscribe({
      next: (res: any) => {
        console.log('API Response:', res);
        this.premiumResult = res;
      },
      error: (err: any) => {
        console.error('Calculation failed:', err);
        alert('Failed to calculate premium. Check console for details.');
      }
    });
  }
}
