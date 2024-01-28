import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FollowingUserPostsComponent } from './following-user-posts.component';

describe('FollowingUserPostsComponent', () => {
  let component: FollowingUserPostsComponent;
  let fixture: ComponentFixture<FollowingUserPostsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FollowingUserPostsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FollowingUserPostsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
