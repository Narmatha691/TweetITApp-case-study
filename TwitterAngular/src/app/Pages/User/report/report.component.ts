import { Component } from '@angular/core';
import { ReportDTO } from '../../../Models/Report/report-dto';
import { HttpClient,HttpClientModule,HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-report',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './report.component.html',
  styleUrl: './report.component.css'
})
export class ReportComponent {
unReadReports:ReportDTO[]=[];
ReadReports:ReportDTO[]=[];
MyReports:ReportDTO[]=[];
report:ReportDTO;
Role:any;
UserId:any;

httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'Bearer ' + localStorage.getItem('token'),
  }),
};
constructor(private http:  HttpClient,private router:Router){
  if(localStorage.getItem('role')==null){
    this.router.navigateByUrl('**');
  }
  this.Role=localStorage.getItem('role');
  this.UserId=localStorage.getItem('userId');
  this.report=new ReportDTO();
  this.getUnReadReports();
  this.getReadReports();
  this.getMyReports();
}

getMyReports(){
  this.http
    .get<ReportDTO[]>('http://localhost:5250/api/Report/GetReportsByUser/'+this.UserId,this.httpOptions)
    .subscribe((response)=>{
      this.MyReports=response;
      console.log(this.MyReports);
    })
}

getUnReadReports(){
  this.http
    .get<ReportDTO[]>('http://localhost:5250/api/Report/GetUnReadReports',this.httpOptions)
    .subscribe((response)=>{
      this.unReadReports=response;
      console.log(this.unReadReports);
    })
}

getReadReports(){
  this.http
    .get<ReportDTO[]>('http://localhost:5250/api/Report/GetReadReports',this.httpOptions)
    .subscribe((response)=>{
      this.ReadReports=response;
      console.log(this.ReadReports);
    })
}

markAsRead(reportId: any) {
  console.log(reportId);
  this.http.put('http://localhost:5250/api/Report/MarkAsRead/' + reportId,reportId,this.httpOptions)
    .subscribe(() => {
      this.getUnReadReports();
      this.getReadReports();
    });
}
}
