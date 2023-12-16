using Android.Content;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Bumptech.Glide;
using Bumptech.Glide.Load.Resource.Drawable;
using Bumptech.Glide.Request;
using Com.Nguyenhoanglam.Imagepicker.Model;
using System.Collections.Generic;

namespace DotnetAndroidImagePickerQs
{
    public class ImageAdapter(Context context) : RecyclerView.Adapter
    {
        private readonly List<Image> images = new();
        private readonly RequestOptions options = new RequestOptions()
            .Placeholder(Resource.Drawable.img_loading_placeholder);

        public void SetImages(IEnumerable<Image> images)
        {
            this.images.Clear();

            if (images is not null)
            {
                this.images.AddRange(images);
            }

            NotifyDataSetChanged();
        }

        public override int ItemCount => images.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is not ImageViewHolder imageViewHolder)
            {
                return;
            }

            var image = images[position];
            Glide.With(context)
                .Load(image.Uri)
                .Apply(options)
                .Transition(DrawableTransitionOptions.WithCrossFade())
                .Into(imageViewHolder.Image);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var inflater = LayoutInflater.FromContext(context);
            var itemView = inflater.Inflate(Resource.Layout.item_image, null);
            return new ImageViewHolder(itemView);
        }
    }

    public class ImageViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; private set; }

        public ImageViewHolder(View itemView) : base(itemView)
        {
            Image = itemView.FindViewById<ImageView>(Resource.Id.image_thumbnail);
        }
    }
}
