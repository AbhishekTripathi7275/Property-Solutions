import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PropertiesService } from '../../services/properties.service';
import { Property } from '../../model/property';

@Component({
  selector: 'app-property-detail',
  templateUrl: './property-detail.component.html',
  styleUrls: ['./property-detail.component.css'],
})
export class PropertyDetailComponent implements OnInit {
  public propertyId: number | undefined;
  property = new Property();
  public MainPhotoUrl!: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private housingService: PropertiesService
  ) {}

  ngOnInit() {
    this.propertyId = Number(this.route.snapshot.params['id']);

    this.route.params.subscribe((params) => {
      this.propertyId = +params['id'];
      this.housingService.getProperty(this.propertyId).subscribe(
        (data: Property | any) => {
          this.property = data!;
          this.property.age=this.housingService.getpropertyAge(new Date(this.property.estPossessionOn!));
          console.log(this.property.photos); 
          for(const photo of this.property.photos!){
            if(photo.isPrimary){
              this.MainPhotoUrl = photo.imageUrl;
            }
          }
        },
        (error) => this.router.navigate(['/'])
      );
    });  
  }
}
