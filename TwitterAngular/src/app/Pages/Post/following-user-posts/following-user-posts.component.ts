import { Component } from '@angular/core';
import { PostWithId } from '../../../Models/Post/post-with-id';
import { HttpClient,HttpClientModule, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router,RouterLink, RouterOutlet} from '@angular/router';
import { UserDTO } from '../../../Models/User/user-dto';

@Component({
  selector: 'app-following-user-posts',
  standalone: true,
  imports: [CommonModule,HttpClientModule,FormsModule,RouterLink,RouterOutlet],
  templateUrl: './following-user-posts.component.html',
  styleUrl: './following-user-posts.component.css'
})
export class FollowingUserPostsComponent {
  post:PostWithId;
  user:UserDTO;
  userRole?:any;
  userId?:any;
  searchTerm?:string='';
  posts:PostWithId[]=[];
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    }),
  };
  constructor(private http:HttpClient,private router:Router){
    if(localStorage.getItem('role')==null){
      this.router.navigateByUrl('**');
    }
    this.userId=localStorage.getItem('userId');
    this.userRole=localStorage.getItem('role');
    this.post=new PostWithId();
    this.user=new UserDTO();
    this.getAllPosts();
  }
  getAllPosts(){
    // console.log(this.userId);
    this.http
    .get<PostWithId[]>('http://localhost:5250/api/Post/GetPostsOfFollowing/'+this.userId,this.httpOptions)
    .subscribe((response)=>{
      this.posts=response;
      // console.log(this.posts);
    })
  }

  onSearch(): void{
    // console.log(this.searchName);
    if(this.searchTerm==''){
      this.getAllPosts();
    }
    else{
      this.http
      .get<PostWithId[]>('http://localhost:5250/api/Post/SearchPostsOfFollowing/'+this.userId+'/'+this.searchTerm,this.httpOptions)
      .subscribe((response)=>{
        this.posts=response;
        //console.log(this.posts);
      })
    }
  }

  public createImgPath = (url: any) => { 
    return `http://localhost:5250/${url}`;
  }

  viewpost(postId:any){
    console.log(postId);
    if(this.userRole=='User')
      this.router.navigateByUrl('user-dashboard/view-post/'+postId);
    else
      this.router.navigateByUrl('admin-dashboard/view-post/'+postId);
  }
}
