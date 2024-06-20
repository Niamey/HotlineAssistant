import { BaseCollectionInterface } from '../../interfaces/base/baseCollection.interface';
import { PagingInterface } from '../../interfaces/common/paging.interface';

export default class PagedModel {
  private collection: Array<BaseCollectionInterface>;
  private readonly paging: PagingInterface;

  constructor (data: Array<BaseCollectionInterface>) {
    this.collection = data;
    this.paging = {
      pageSize: 3,
      pageIndex: 1,
      totalRows: 0,
      isAvailableNextPage: false
    };
  }

  public get Count () {
    return this.collection.length;
  }

  public getCount () {
    return this.collection.length;
  }

  public getPaged (pageIndex: number, pageSize: number): Array<BaseCollectionInterface> {
    this.paging.pageIndex = pageIndex > 0 ? pageIndex - 1 : pageIndex;
    this.paging.pageSize = pageSize < 1 ? 1 : pageSize;
    const result = [...(this.collection.filter((value, n) => {
      return (n >= (this.paging.pageIndex * this.paging.pageSize)) && (n < ((this.paging.pageIndex + 1) * this.paging.pageSize));
    }))];
    this.paging.isAvailableNextPage = this.paging.totalRows > result.length && result.length !== this.paging.pageSize;

    return result;
  }

  public getPager () : PagingInterface {
    return {
      totalRows: this.collection.length,
      pageIndex: this.paging.pageIndex,
      pageSize: this.paging.pageSize,
      isAvailableNextPage: this.paging.isAvailableNextPage
    };
  }
}
