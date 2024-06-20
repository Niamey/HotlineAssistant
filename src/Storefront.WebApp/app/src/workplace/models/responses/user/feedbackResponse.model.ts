import { FeedbackResponseInterface } from '../../../interfaces/responses/user/feedbackResponse.interface';

export default class FeedbackResponseModel implements FeedbackResponseInterface {
  constructor (data :FeedbackResponseInterface) {
    this.success = data.success;
    this.message = data.message;
  }

  public success: boolean;
  public message?: string;
}
