import { Component } from '@angular/core';
import { ReportDTO } from '../../../Models/Report/report-dto';
import { HttpClient,HttpClientModule,HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-report',
  standalone: true,
  imports: [],
  templateUrl: './report.component.html',
  styleUrl: './report.component.css'
})
export class ReportComponent {
reports:ReportDTO[]=[];
report:ReportDTO;
constructor(private http:  HttpClient,private router:Router){
  this.report=new ReportDTO();

}
}
