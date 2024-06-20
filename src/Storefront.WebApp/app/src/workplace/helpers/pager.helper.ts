import { BaseCollectionInterface } from '../interfaces/base/baseCollection.interface';
import PagedModel from '../models/paging/paged.model';

export default class PagerHelper {
  public model: PagedModel;

  constructor (list: Array<BaseCollectionInterface>) {
    this.model = new PagedModel(list);
  }

  public getCount () : number {
    return this.model.getCount();
  }

  public getPaged (pageIndex: number, pageSize: number) : Array<BaseCollectionInterface> {
    return this.model.getPaged(pageIndex, pageSize);
  }

  /* public getPaged (list: Array<BaseCollectionInterface>, pageNumber: number, pageSize: number): Array<BaseCollectionInterface> {
    pageNumber = pageNumber > 0 ? pageNumber - 1 : pageNumber;
    pageSize = pageSize < 1 ? 1 : pageSize;

    return [...(list.filter((value, n) => {
      return (n >= (pageNumber * pageSize)) && (n < ((pageNumber + 1) * pageSize));
    }))];
  } */
}
