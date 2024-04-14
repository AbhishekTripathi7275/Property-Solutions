import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { IPropertyBase } from '../model/ipropertybase';
import { Property } from '../model/property';
import { Observable } from 'rxjs';
import { Ikeyvaluepair } from '../model/ikeyvaluepair';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PropertiesService {
  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) {}

  getAllCities(): Observable<string[]> {
    return this.http.get<string[]>(this.baseUrl + '/city/cities');
  }

  getPropertyTypes(): Observable<Ikeyvaluepair[]> {
    return this.http.get<Ikeyvaluepair[]>(this.baseUrl + '/propertytype/list');
  }

  getFurnishingTypes(): Observable<Ikeyvaluepair[]> {
    return this.http.get<Ikeyvaluepair[]>(
      this.baseUrl + '/furnishingtype/list'
    );
  }

  getProperty(id: number) {
    return this.http.get<Property[]>(
      this.baseUrl + '/property/detail/' + id?.toString()
    );
  }

  getAllProperties(SellRent?: number): Observable<Property[]> {
    return this.http.get<Property[]>(
      this.baseUrl + '/property/list/' + SellRent?.toString()
    );
  }

  addProperty(property: Property) {
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token'),
      }),
    };

    return this.http.post(
      this.baseUrl + '/property/add',
      property,
      httpOptions
    );
  }

  uploadImage(image: FileList, PropertyId: number) {
    const formData: FormData = new FormData();
    for (let i = 0; i < image.length; i++) {
      formData.append('file', image[i]);
    }

    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('token'),
      }),
    };

    return this.http.post(
      this.baseUrl + '/property/add/photo/' + String(PropertyId),
      formData,
      httpOptions
    );
  }

  getpropertyAge(dateofEstablishment: Date): string {
    const today = new Date();
    const estDate = new Date(dateofEstablishment);
    let age = today.getFullYear() - estDate.getFullYear();
    const m = today.getMonth() - estDate.getMonth();

    if (m < 0 || (m == 0 && today.getDate() < estDate.getDate())) {
      age--;
    }

    if (today < estDate) {
      return '0';
    }

    if (age === 0) {
      return 'Less than a year';
    }

    return age.toString();
  }

  newPropID() {
    if (localStorage.getItem('PID')) {
      localStorage.setItem('PID', String(+localStorage.getItem('PID')! + 1));
      return +localStorage.getItem('PID')!;
    } else {
      localStorage.setItem('PID', '101');
      return 101;
    }
  }
}
