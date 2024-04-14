import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PropertiesService } from '../../services/properties.service';
import { IPropertyBase } from '../../model/ipropertybase';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrl: './property-list.component.css',
})
export class PropertyListComponent implements OnInit {
  SellRent = 1;
  properties: Array<IPropertyBase> = [];
  City = '';
  SearchCity = '';
  SortbyParam = '';
  SortDirection = 'asc';

  constructor(
    private route: ActivatedRoute,
    private propertiesService: PropertiesService
  ) {}

  ngOnInit(): void {
    if (this.route.snapshot.url.toString()) {
      this.SellRent = 2;
    }

    this.propertiesService.getAllProperties(this.SellRent).subscribe(
      (data) => {
        this.properties = data;

        // const newProperty = JSON.parse(localStorage.getItem('newProp') || '{}');
        // if (newProperty.SellRent === this.SellRent) {
        //   this.properties = [newProperty, ...this.properties];
        // }

        console.log(data);
        console.log(this.route.snapshot.url.toString());
      },
      (error) => {
        console.log(error);
      }
    );
  }

  onCityFilter() {
    this.SearchCity = this.City;
  }
  onCityFilterClear() {
    this.SearchCity = '';
    this.City = '';
  }

  onSortDirection() {
    if (this.SortDirection === 'desc') {
      this.SortDirection = 'asc';
    } else {
      this.SortDirection = 'desc';
    }
  }
}
