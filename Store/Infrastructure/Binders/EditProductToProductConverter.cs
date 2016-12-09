using Store.Domain.Entities;
using Store.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using Store.Models;

namespace Store.Infrastructure.Binders
{
    public class EditProductToProductConverter : AutoMapper.ITypeConverter<EditProductViewModel, Product>
    {
        public Product Convert(EditProductViewModel source, Product destination, ResolutionContext context)
        {
            //destination.ImageData = convertToByteArray(source.Image);
            return destination;
        }

        public byte[] convertToByteArray(HttpPostedFileBase file)
        {
            byte[] image = null;
            BinaryReader reader = new BinaryReader(file.InputStream);
            image = reader.ReadBytes(file.ContentLength);
            return image;
        }
    }
}