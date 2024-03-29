import { Component } from '@angular/core';
import { UserDTO } from '../../../Models/User/user-dto';
import { ActivatedRoute ,Router} from '@angular/router';
import { HttpClient,HttpClientModule ,HttpHeaders} from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Report } from '../../../Models/Report/report';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule,HttpClientModule,FormsModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
  user:UserDTO;
  userId:string="";
  role?:any;
  report:Report;
  errMsg: string = '';
  isUserExist: boolean = false;
  following:boolean=false;
  loggedinUserId?:any;
  textInput: string = '';
  isEmpty: boolean = false;
  reportbool:boolean=false;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    }),
  };
  constructor(private activateRoute:ActivatedRoute,private http:HttpClient,private router:Router){
    if(localStorage.getItem('role')==null){
      this.router.navigateByUrl('**');
    }
    this.user=new UserDTO();
    this.report=new Report();
    this.role=localStorage.getItem('role');
    this.activateRoute.params.subscribe((p) => (this.userId = p['uid']));
    // console.log(this.userId);
    this.search();
  }
  search() {
    this.http
      .get<UserDTO>(
        'http://localhost:5250/api/User/GetUserById/' + this.userId,this.httpOptions
      )
      .subscribe((response) => {
        // console.log(response);
        if (response != null) {
          this.user = response;
          // console.log(this.user);
          this.isUserExist = true;
          this.errMsg = '';
        } else {
          this.errMsg = 'Invalid User Id';
          this.isUserExist = false;
        }
        this.checkFollowing();
      });
  }
  followRequest(userID:any){
      const requestBody = {
        userId: userID,
        followerId: localStorage.getItem('userId'),
      };
      this.http
      .post('http://localhost:5250/api/Follower/SendFollowRequest/',requestBody,this.httpOptions)
      .subscribe((response) => {
        this.following=false;
        console.log(this.following);
        console.log(response);
      });
  }

  checkFollowing(){
    const requestBody = {
      userId: this.userId,
      followerId: localStorage.getItem('userId'),
    };
    // console.log(this.user);
    if(this.role=='Admin'){
      this.following=false;
    }
    else if(this.user.type=='Normal'){
      this.following=false;
    }
    else if(requestBody.userId==requestBody.followerId){
      this.following=false;
    }
    else{
      this.http
      .put('http://localhost:5250/api/Follower/CheckFollowing',requestBody,this.httpOptions)
      .subscribe((response) => {
        console.log(response);
        if(response==true){
          this.following=false;
        }
        else{
          this.following=true;
        }
      });
    }
  }


  submit(userId:any) {
    this.report.reportContent=this.textInput;
    this.report.reportUserId=this.userId;
    this.report.senderId=localStorage.getItem('userId');
    this.isEmpty = this.textInput.trim() === '';
    if(!this.isEmpty){
      console.log(this.report);
      this.http
      .post('http://localhost:5250/api/Report/AddReport',this.report,this.httpOptions)
      .subscribe((response) => {
        console.log(response);
      });
      this.reportbool=false;
    }
  }
  reportfn(){
    
    this.reportbool=true;
    
  }
}
