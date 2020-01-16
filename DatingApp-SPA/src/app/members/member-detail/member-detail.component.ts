import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  user: User;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor(private userService: UserService,
              private alertify: AlertifyService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data.user; // data['user'] passed object in routes.ts or routing module
    });
    // this.loadUser();

    this.galleryOptions = [
      {
          width: '500px',
          height: '500px',
          thumbnailsPercent: 20,
          thumbnailsColumns: 4,
          imageAnimation: NgxGalleryAnimation.Slide,
          preview: false
      }
    ];

    this.galleryImages = this.getImages();
  }

  getImages() {
    const imageUrl = [];

    // tslint:disable-next-line:prefer-for-of
    for (let i = 0; i < this.user.photos.length; i++) {
      imageUrl.push({
        small: this.user.photos[i].url,
        medium: this.user.photos[i].url,
        big: this.user.photos[i].url,
        description: this.user.photos[i].description
      });
    }
    return imageUrl;
  }
  // members/4   /*ActivateRoute can access this*/
  /*loadUser() {
    // + to convert to number
    // deprecated this.route.snapshot.params.['id']
    // use save navigation operator to avoid errors
   this.userService.getUser(+this.route.snapshot.params.id).subscribe((user: User) => {
   this.user = user;
   }, error => {
     this.alertify.error(error);
   });
  }*/

}
